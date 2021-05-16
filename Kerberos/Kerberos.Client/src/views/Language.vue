<template>
    <section>
        <h1 class="mb-5">Sprachen</h1>
        <b-modal
            id="form-modal"
            title="Sprache hinzufügen"
            centered
            v-model="showModal"
            size="lg"
            ok-title="Absenden"
            ok-variant="success"
            cancel-title="Abbrechen"
            @ok="onModalSubmit"
        >
            <b-form>
                <div class="row">
                    <div class="col">
                        <b-form-group
                            id="input-key"
                            label="Sprachschlüssel"
                            label-for="key-input"
                        >
                            <b-input
                                id="key-input"
                                v-model="formItem.key"
                                type="text"
                                required
                                placeholder="de-DE"
                            ></b-input>
                        </b-form-group>
                    </div>
                    <div class="col">
                        <b-form-group
                            id="input-name"
                            label="Name"
                            label-for="name-input"
                        >
                            <b-input
                                id="name-input"
                                v-model="formItem.name"
                                type="text"
                                required
                                placeholder="Deutsch"
                            ></b-input>
                        </b-form-group>
                    </div>
                </div>
            </b-form>
        </b-modal>

        <div class="mt-3">
            <span class="mr-2">Optionen:</span>
            <b-button-group>
                <b-button variant="outline-success" @click="addButtonClicked"
                    >Neues Element hinzufügen</b-button
                >
                <b-button variant="outline-danger" @click="deleteButtonClicked"
                    >Ausgewältes Element löschen</b-button
                >
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
            <p
                v-if="items.length == 0 && !loading && !error"
                class="text-center"
            >
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
import axios from "axios";
import Vue from "vue";
import { Language } from "../models/models";
const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.component("Language", {
    data() {
        return {
            loading: false,
            items: [] as Language[],
            fields: [
                { key: "key", sortable: true, label: "Sprachschlüssel" },
                { key: "name", sortable: true, label: "Sprachname" }
            ],
            error: false,
            errorMsg: "Fehler...",
            formItem: {} as Language,
            selectedItem: {} as Language,
            showModal: false
        };
    },
    mounted() {
        this.loadLangaugeItems();
    },
    methods: {
        loadLangaugeItems() {
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/api/languages";
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
        inputValid() {
            return this.formItem.key != "" && this.formItem.name != "";
        },
        onModalSubmit(event: any) {
            if (!this.inputValid()) {
                event.preventDefault();
                return;
            }

            const url = VUE_APP_API_BASE_URL + "/api/languages";
            console.log(url);
            axios
                .post(url, this.formItem)
                .then(response => {
                    const language = response.data;
                    this.items.push(language);
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        },
        addButtonClicked(event: any) {
            this.showModal = !this.showModal;
            this.formItem = {} as Language;
            this.formItem.id = 0;
            this.formItem.key = "";
            this.formItem.name = "";
        },
        deleteButtonClicked(event: any) {
            if (!this.selectedItem || !this.selectedItem.id) return;

            const url =
                VUE_APP_API_BASE_URL + "/api/languages/" + this.selectedItem.id;
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
        onRowSelected(item: Language[]) {
            this.selectedItem = item[0];
        },
        showErrorMessage(message: string){
            this.error = true;
            this.errorMsg = message;
        }
    }
});
</script>

<style lang="scss"></style>
