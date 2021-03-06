<template>
    <section>
        <h1 class="mb-5">Anrede</h1>
        <b-modal
            id="form-modal"
            title="Anrede hinzufügen"
            centered
            v-model="showModal"
            size="lg"
            ok-title="Absenden"
            ok-variant="success"
            cancel-title="Abbrechen"
            @ok="onModalSubmit"
            @show="resetModal"
        >
            <div class="row">
                <div class="col">
                    <b-form-group label="Anrede">
                        <b-input placeholder="Herr" v-model="formItem.value"></b-input>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <b-form-group label="Formale Anrede">
                        <b-input placeholder="Sehr geehrter Herr" v-model="formItem.formalSalutation"></b-input>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <b-form-group label="Sprache">
                        <b-form-select
                            v-model="formItem.language"
                            :options="languageOptions"
                        ></b-form-select>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <b-form-group label="Geschlecht">
                        <b-form-select
                            v-model="formItem.gender"
                            :options="genderOptions"
                        ></b-form-select>
                    </b-form-group>
                </div>
            </div>

            <div class="row mt-3" v-if="modalError">
                <div class="col">
                    <b-card
                        border-variant="danger"
                        header-border-variant="danger"
                        header-text-variant="danger"
                        text-variant="danger"
                        align="center"
                    >
                        <b-card-text>{{ modalErrorMsg }}</b-card-text>
                    </b-card>
                </div>
            </div>
        </b-modal>

        <div class="mt-3">
            <span class="mr-2">Optionen:</span>
            <b-button-group>
                <b-button variant="outline-success" @click="addButtonClicked"
                    >Neue Anrede hinzufügen</b-button
                >
                <b-button variant="outline-danger" @click="deleteButtonClicked"
                    >Ausgewälte Anrede löschen</b-button
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
import { Gender, Language, Salutation, Title } from "../models/models";
const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.component("Salutation", {
    data() {
        return {
            loading: false,
            items: [] as Salutation[],
            languages: [] as Language[],
            formItem: {} as Salutation,
            fields: [
                { key: "value", sortable: true, label: "Anrede" },
                {
                    key: "formalSalutation",
                    sortable: true,
                    label: "Formale Anrede"
                },
                {
                    key: "language",
                    sortable: true,
                    label: "Sprache",
                    formatter: (value: any, key: any, item: Salutation) => {
                        return item.language.name
                    }
                },
                {
                    key: "gender",
                    sortable: false,
                    label: "Geschlecht",
                    formatter: (value: any, key: any, item: Salutation) => {
                        if (item.gender == Gender.MALE) return "Männlich"
                        if (item.gender == Gender.FEMALE) return "Weiblich"
                        if (item.gender == Gender.DIVERSE) return "Divers"
                    }
                }
            ],
            error: false,
            errorMsg: "Fehler...",

            selectedItem: {} as Salutation,
            showModal: false,

            modalError: false,
            modalErrorMsg: "Fehler...",

            genderOptions: [
                {value: Gender.MALE, text: "Männlich"},
                {value: Gender.FEMALE, text: "Weiblich"},
                {value: Gender.DIVERSE, text: "Divers"},
            ]
        };
    },

    computed: {
        // computes language options for the dropdown
        languageOptions(): any {
            return this.languages.map(language => {
                return {
                    value: language,
                    text: language.name
                };
            });
        }
    },

    // loads the languages and salutation items on page load
    mounted() {
        this.loadSalutationItems();
        this.loadLanguages();
    },

    methods: {
        // loads salutation items from the api
        loadSalutationItems() {
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/salutations";
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

        // loads language items from the api
        loadLanguages() {
            const url = `${VUE_APP_API_BASE_URL}/languages`;
            axios
                .get(url)
                .then(response => {
                    this.languages = response.data;
                })
                .catch(error => {
                    console.error(error);
                    this.error = true;
                    this.errorMsg = error;
                });
        },

        // validates the form data and submits it to the api
        async onModalSubmit(event: any) {
            if (!this.inputValid()) {
                event.preventDefault();
                this.modalError = true;
                this.modalErrorMsg = "Bitte füllen Sie alle Felder aus"
                return;
            }

            try {
                const url = `${VUE_APP_API_BASE_URL}/salutations`
                const response = await axios.post(url, {
                    value: this.formItem.value,
                    formalSalutation: this.formItem.formalSalutation,
                    languageId: this.formItem.language.id,
                    gender: this.formItem.gender
                })
                this.loadSalutationItems();
                this.loadLanguages();
                this.resetModal();
            } catch (error){
                console.error("Failed to create salutation", error);
            }
        },

        // validates form inputs
        inputValid() {
            return this.formItem.value !== '' && this.formItem.formalSalutation !== '' && this.formItem.language != null && this.formItem.gender != null
        },

        resetModal() {
            this.formItem = {} as Salutation
        },

        addButtonClicked(event: any) {
            this.showModal = true;
        },

        // sends delete request for salutations to the api
        deleteButtonClicked(event: any) {
            if (!this.selectedItem || !this.selectedItem.id) return;

            const url =
                VUE_APP_API_BASE_URL +
                "/salutations/" +
                this.selectedItem.id;
            axios
                .delete(url)
                .then(response => {
                    if (response.status == 200) {
                        this.loadSalutationItems();
                        this.loadLanguages();
                    } else {
                        throw Error('Non 200 status code returend from server.');
                    }
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        },

        onRowSelected(item: Salutation[]) {
            this.selectedItem = item[0];
        },

        // shows error message
        showErrorMessage(message: string) {
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
