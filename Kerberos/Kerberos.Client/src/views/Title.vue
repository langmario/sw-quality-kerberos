<template>
	<section>
		<h1 class="mb-5">Titel</h1>
		<b-modal
			id="form-modal"
			title="Titel hinzufügen"
			centered
			v-model="showModal"
			size="lg"
			ok-title="Absenden"
			ok-variant="success"
			cancel-title="Abbrechen"
			@ok="onModalSubmit"
			@show="resetModal"
		>
			<b-form-group label="Titel">
				<b-input placeholder="Titel" v-model="newTitle" />
			</b-form-group>

			<b-button @click="addAlias">Alias hinzufügen</b-button>

			<hr />

			<div class="aliases">
				<b-form-group v-for="(alias, index) in newTitleAliases" :key="index">
					<b-input v-model="newTitleAliases[index]" placeholder="Alias" />
				</b-form-group>
			</div>
		</b-modal>

		<div class="mt-3">
			<span class="mr-2">Optionen:</span>
			<b-button-group>
				<b-button variant="outline-success" @click="addButtonClicked">Neues Element hinzufügen</b-button>
				<b-button variant="outline-danger" @click="deleteButtonClicked">Ausgewältes Element löschen</b-button>
			</b-button-group>
		</div>

		<div class="mt-3">
			<div v-if="loading">
				Lade Daten. Bitte warten...<br /><br />
				<b-spinner />
			</div>
			<b-table
				v-if="items.length != 0"
				bordered
				hover
				striped
				:items="items"
				:fields="fields"
				select-mode="single"
				responsive="sm"
				ref="selectableTable"
				selectable
				@row-selected="onRowSelected"
			>
			</b-table>
			<p v-if="items.length == 0 && !loading && !error" class="text-center">
				Keine Daten vorhanden
			</p>
			<b-card
				v-if="error"
				border-variant="danger"
				header="Es ist ein Fehler aufgetreten"
				header-border-variant="danger"
				header-text-variant="danger"
				align="center"
			>
				<b-card-text>
					{{ errorMsg }}
				</b-card-text>
			</b-card>
		</div>
	</section>
</template>

<script lang="ts">
import axios from 'axios';
import Vue from 'vue';
import { Title, TitleAlias } from '../models/models';
const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.component('Title', {
	data() {
		return {
			loading: false,
			items: [] as Title[],
			fields: [
				{ key: 'value', sortable: true, label: 'Titel' },
				{ key: 'aliasList', sortable: false, label: 'Titel-Alias' },
			],
			error: false,
			errorMsg: 'Fehler...',

			newTitle: '',
			newTitleAliases: [] as string[],

			selectedItem: {} as Title,
			showModal: false,
		};
	},
	mounted() {
		this.loadTitleItems();
	},
	methods: {
		loadTitleItems() {
			this.loading = true;
			const url = VUE_APP_API_BASE_URL + '/api/titles';
			axios
				.get(url)
				.then(response => {
					this.items = response.data;
					this.loading = false;
				})
				.catch(error => {
					console.error(error);
					this.loading = false;
					this.error = true;
					this.errorMsg = error;
				});
		},
		async onModalSubmit(event: any) {
			if (this.newTitle) {
				try {
					const response = await axios.post(`${VUE_APP_API_BASE_URL}/api/titles`, {
						name: this.newTitle,
					});

					const createdTitle = response.data;

					for (const alias of this.newTitleAliases) {
						await axios.post(`${VUE_APP_API_BASE_URL}/api/titles/${createdTitle.id}/aliases`, {
							alias,
						});
					}
				} catch (error) {
					console.error('Failed to create title', error);
				}
			}
		},
		resetModal() {
			this.newTitle = '';
			this.newTitleAliases = [];
		},
		addButtonClicked(event: any) {
			this.showModal = true;
		},
		addAlias() {
			this.newTitleAliases.push('');
		},
		deleteButtonClicked(event: any) {
			if (!this.selectedItem || !this.selectedItem.id) return;

			const url = VUE_APP_API_BASE_URL + '/api/titles/' + this.selectedItem.id;
			axios
				.delete(url)
				.then(response => {
					if (response.status == 200) {
						const index = this.items.indexOf(this.selectedItem);
						this.items.splice(index, 1);
					} else {
						// TODO: error handling
					}
				})
				.catch(error => {
					this.showErrorMessage(error);
					console.error(error);
				});
		},
		onRowSelected(item: Title[]) {
			this.selectedItem = item[0];
		},
		showErrorMessage(message: string) {
			this.error = true;
			this.errorMsg = message;
		},
	},
});
</script>

<style lang="scss"></style>
