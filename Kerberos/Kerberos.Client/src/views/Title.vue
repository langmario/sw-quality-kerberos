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
        >
            <b-form>
                <div class="row">
                    <div class="col">
                        <b-form-group
                            id="input-value"
                            label="Titel"
                            label-for="value-input"
                        >
                            <b-input
                                id="value-input"
                                v-model="formItem.value"
                                type="text"
                                required
                                placeholder="Herr"
                            ></b-input>
                        </b-form-group>
                    </div>
                </div>
                <div
                    class="row"
                    v-for="(title, index) in this.formItem.aliasList"
                    :key="title.value"
                >
                    <div class="col-11">
                        <b-form-group
                            id="input-title-alias"
                            :label="'Titel-Alias ' + (index + 1)"
                            label-for="title-alias-input"
                        >
                            <b-input
                                :id="'title-alias-input-' + index"
                                v-model="title.value"
                                type="text"
                                required
                                placeholder="Hr."
                            ></b-input>
                        </b-form-group>
                    </div>
                    <div class="col-1">
                        <b-button
                            variant="outline-danger"
                            @click="removeAlias(index)"
                            >-</b-button
                        >
                    </div>
                </div>
                <div class="row">
                    <div class="col">
                        <b-button variant="outline-success" @click="addAlias"
                            >Weiteren Titel Alias hinzufügen</b-button
                        >
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
import { Title, TitleAlias } from "../models/models";
const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.component("Title", {
    data() {
        return {
            loading: false,
            items: [] as Title[],
            fields: [
                { key: "value", sortable: true, label: "Titel" },
                { key: "aliasList", sortable: false, label: "Titel-Alias" }
            ],
            error: false,
            errorMsg: "Fehler...",
            formItem: {
                id: 0,
                value: "",
                aliasList: [{ id: 0, value: "" } as TitleAlias]
            } as Title,
            selectedItem: {} as Title,
            showModal: false
        };
    },
    mounted() {
        this.loadTitleItems();
    },
    methods: {
        loadTitleItems() {
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/api/titles";
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
            return this.formItem.value != "";
        },
        onModalSubmit(event: any) {
            if (!this.inputValid()) {
                event.preventDefault();
                return;
            }

            const url = VUE_APP_API_BASE_URL + "/api/titles";
            console.log(url);
            axios
                .post(url, this.formItem)
                .then(response => {
                    const item: Title = response.data;
                    this.items.push(item);
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        },
        addButtonClicked(event: any) {
            this.showModal = !this.showModal;
            this.formItem = {} as Title;
            this.formItem.id = 0;
            this.formItem.value = "";
            this.formItem.aliasList = [];
        },
        deleteButtonClicked(event: any) {
            if (!this.selectedItem || !this.selectedItem.id) return;

            const url =
                VUE_APP_API_BASE_URL + "/api/titles/" + this.selectedItem.id;
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
        addAlias(event: any) {
            if (
                this.formItem.aliasList.length != 0 &&
                this.formItem.aliasList[this.formItem.aliasList.length - 1]
                    .value == ""
            ) {
                return;
            }
            this.formItem.aliasList.push({
                id: 0,
                value: ""
            });
        },
        removeAlias(index: number) {
            this.formItem.aliasList.splice(index, 1);
        }
    }
});
</script>

<style lang="scss"></style>
