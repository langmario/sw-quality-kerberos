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
                                placeholder="de"
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
                    >Neue Sprache hinzufügen</b-button
                >
                <b-button variant="outline-danger" @click="deleteButtonClicked"
                    >Ausgewälte Sprache löschen</b-button
                >
            </b-button-group>
        </div>

        <div class="mt-3">
            <div v-if="loading" class="loading">
                <div class="loading-item">Lade Daten. Bitte warten...</div>
                <div class="loading-item mt-3"><b-spinner></b-spinner></div>
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
                header="Fehler"
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

    // load language items when page is loaded
    mounted() {
        this.loadLangaugeItems();
    },

    methods: {
        // loads language items from api
        loadLangaugeItems() {
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/languages";
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

        // validates the input in the form in the modal
        inputValid() {
            return this.formItem.key != "" && this.formItem.name != "";
        },

        // submits the form from the modal to the api
        onModalSubmit(event: any) {
            if (!this.inputValid()) {
                event.preventDefault();
                return;
            }

            const url = VUE_APP_API_BASE_URL + "/languages";
            console.log(url);
            axios
                .post(url, this.formItem)
                .then(response => {
                    this.loadLangaugeItems();
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        },

        // opens the modal and shows the form
        addButtonClicked(event: any) {
            this.showModal = !this.showModal;
            this.formItem = {} as Language;
            this.formItem.id = 0;
            this.formItem.key = "";
            this.formItem.name = "";
        },

        // sends the delete request for the selected item to the api
        deleteButtonClicked(event: any) {
            if (!this.selectedItem || !this.selectedItem.id) return;

            const url =
                VUE_APP_API_BASE_URL + "/languages/" + this.selectedItem.id;
            axios
                .delete(url)
                .then(response => {
                    if (response.status == 200) {
                        this.loadLangaugeItems();
                    } else {
                        throw Error('Non 200 status code returend from server.');
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

        // shows error message
        showErrorMessage(message: string){
            this.error = true;
            this.errorMsg = message;
        }
    }
});
</script>

<style lang="scss">
.loading{
    display: flex;
    flex-direction: column;

    .loading-item {
        display: flex;
        justify-content: center;
    }
}
</style>

