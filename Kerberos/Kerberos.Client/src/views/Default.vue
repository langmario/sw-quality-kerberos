<template>
	<div class="home">
		<h1 class="mb-5">Eingabe</h1>
		<b-card header="Anrede eingeben">
			<b-form @submit="onSubmit">
				<b-form-group id="input-key" label="Anrede" label-for="key-input">
					<b-input type="text" v-model="input" required placeholder="Herr Max Mustermann"></b-input>
				</b-form-group>
				<b-button variant="primary" type="submit">Senden</b-button>
				<b-button variant="danger" @click="reset" class="ml-2">Reset</b-button>
			</b-form>
		</b-card>
		<b-row class="mt-4" v-if="wasParsed">
			<b-col>
				<b-card header="Anrede">
					<table class="table">
						<tr>
							<td>Anrede</td>
							<th>{{ salutation ? salutation.value : '' }}</th>
						</tr>
						<tr>
							<td>Sprache</td>
							<th>{{ salutation ? salutation.language.name : '' }}</th>
						</tr>
						<tr>
							<td>Geschlecht</td>
							<th>
								{{ salutation ? getGenderName(salutation.gender) : '' }}
							</th>
						</tr>
					</table>
				</b-card>
			</b-col>
			<b-col>
				<b-card header="Titel">
					<b-form-checkbox v-for="title in titles" :key="title.id" :value="title.value" v-model="selectedTitles">{{ title.value }}</b-form-checkbox>
				</b-card>
			</b-col>
			<b-col>
				<b-card header="Name">
					<b-form-group label="Vorname">
						<b-input v-model="firstname" placeholder="Vorname" />
					</b-form-group>
					<b-form-group label="Nachname">
						<b-input v-model="lastname" placeholder="Nachname" />
					</b-form-group>
				</b-card>
			</b-col>
		</b-row>
		<b-card header="Förmliche Briefanrede" class="mt-5" v-if="wasParsed">
			<h4>{{ selectedTitles.join(' ') }} {{ firstname }} {{ lastname }}</h4>
		</b-card>
	</div>
</template>

<script lang="ts">
import Vue from 'vue';
import axios from 'axios';

const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.extend({
	name: 'Default',
	components: {},
	data() {
		return {
			input: '',
			wasParsed: false,
			firstname: null,
			lastname: null,
			titles: null,
			salutation: null,
			selectedTitles: []
		};
	},
	methods: {
		async onSubmit(event: any) {
			event.preventDefault();
			const url = VUE_APP_API_BASE_URL + '/api/parse';
			const result = await axios.post(url, {
				input: this.input,
			});

			const { firstname, lastname, titles, salutation } = result.data;

			this.firstname = firstname;
			this.lastname = lastname;
			this.titles = titles;
			this.salutation = salutation;

			this.wasParsed = true;
		},

		getGenderName(genderId: number) {
			switch (genderId) {
				case 0:
					return 'Männlich';
				case 1:
					return 'Weiblich';
				case 2:
					return 'Divers';
				default:
					return '';
			}
		},

		reset() {
			this.wasParsed = false;
			this.firstname = null;
			this.lastname = null;
			this.titles = null;
			this.salutation = null;
		}
	},
});
</script>
