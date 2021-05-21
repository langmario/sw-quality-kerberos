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
            <div class="row">
                <div class="col">
                    <b-form-group label="Titel">
                        <b-input placeholder="Titel" v-model="newTitle" />
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col"></div>
            </div>

            <!-- <hr /> -->

            <div class="aliases">
                <div
                    class="row"
                    v-for="(alias, index) in newTitleAliases"
                    :key="index"
                >
                    <div class="col-11">
                        <b-form-group>
                            <b-input
                                v-model="newTitleAliases[index]"
                                placeholder="Alias"
                            />
                        </b-form-group>
                    </div>
                    <div class="col-1">
                        <b-button
                            @click="removeAlias(index)"
                            variant="outline-danger"
                            >-</b-button
                        >
                    </div>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <b-button @click="addAlias">Alias hinzufügen</b-button>
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

        <b-modal
            id="title-alias-form-modal"
            title="Titel-Alias hinzufügen"
            centered
            v-model="showTitleAliasModal"
            ok-title="Absenden"
            ok-variant="success"
            cancel-title="Abbrechen"
            @ok="onModalTitleAliasSubmit"
            @show="resetTitleAliasModal"
        >
            <div class="row">
                <div class="col">
                    <b-form-group label="Titel-Alias">
                        <b-input v-model="newTitleAlias.value" required />
                    </b-form-group>
                </div>
            </div>
        </b-modal>

        <div class="mt-3">
            <span class="mr-2">Optionen:</span>
            <b-button-group>
                <b-button variant="outline-success" @click="addButtonClicked"
                    >Neuen Titel hinzufügen</b-button
                >
                <b-button variant="outline-danger" @click="deleteButtonClicked"
                    >Ausgewälten Titel löschen</b-button
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
                <template #cell(titleAlias)="data">
                    <div v-for="item in data.item.aliases" :key="item.id">
                        <span
                            @click="deleteTitleAlias(data.item.id, item.id)"
                            class="text-danger bold-minus"
                            >-</span
                        >
                        <span class="ml-3">
                            {{ item.value }}
                        </span>
                    </div>
                    <div>
                        <b-button
                            variant="outline-success"
                            size="sm"
                            @click="addTitleAliasButtonClicked(data.item.id)"
                            >Neuen Alias hinzufügen</b-button
                        >
                    </div>
                </template>
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
                {
                    key: "titleAlias",
                    sortable: false,
                    label: "Titel-Alias"
                }
            ],
            error: false,
            errorMsg: "Fehler...",

            newTitle: "",
            newTitleAliases: [] as string[],

            selectedItem: {} as Title,
            showModal: false,

            modalError: false,
            modalErrorMsg: "Fehler...",

            showTitleAliasModal: false,
            newTitleAlias: {
                value: "",
                titleId: 0
            }
        };
    },
    mounted() {
        this.loadTitleItems();
    },
    methods: {
        loadTitleItems() {
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/titles";
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
            if (!this.newTitle) return;
            const result = this.inputValid();
            if (!result.valid) {
                event.preventDefault();
                this.modalError = true;
                this.modalErrorMsg = result.message;
                return;
            }

            try {
                const response = await axios.post(
                    `${VUE_APP_API_BASE_URL}/titles`,
                    {
                        name: this.newTitle
                    }
                );

                const createdTitle = response.data;

                for (const alias of this.newTitleAliases) {
                    await axios.post(
                        `${VUE_APP_API_BASE_URL}/titles/${createdTitle.id}/aliases`,
                        {
                            alias
                        }
                    );
                }

                this.loadTitleItems();
            } catch (error) {
                console.error("Failed to create title", error);
            }
        },

        inputValid() {
            let filteredList = this.newTitleAliases.filter(
                titleAlias => titleAlias.trim() === ""
            );
            if (filteredList.length > 0)
                return {
                    valid: false,
                    message: "Titel-Alias Feld darf nicht leer sein"
                };

            filteredList = this.newTitleAliases.filter(
                titleAlias => titleAlias.trim() === this.newTitle.trim()
            );
            if (filteredList.length > 0)
                return {
                    valid: false,
                    message:
                        "Titel-Alias kann nicht den gleichen Text enthalten als zugehöriger Titel"
                };
            return {
                valid: true,
                message: ""
            };
        },

        resetModal() {
            this.newTitle = "";
            this.newTitleAliases = [];
        },

        resetTitleAliasModal() {
            this.newTitleAlias.value = "";
        },

        addButtonClicked(event: any) {
            this.showModal = true;
        },

        addTitleAliasButtonClicked(titleId: number) {
            this.showTitleAliasModal = true;
            this.newTitleAlias.titleId = titleId;
        },

        addAlias() {
            this.newTitleAliases.push("");
        },

        removeAlias(index: number) {
            this.newTitleAliases.splice(index, 1);
        },

        deleteButtonClicked(event: any) {
            if (!this.selectedItem || !this.selectedItem.id) return;

            const url =
                VUE_APP_API_BASE_URL + "/titles/" + this.selectedItem.id;
            axios
                .delete(url)
                .then(response => {
                    if (response.status == 200) {
                        this.loadTitleItems();
                    } else {
                        throw Error("Non 200 status code returned from server");
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

        onModalTitleAliasSubmit(event: any) {
            if (this.newTitleAlias.value === '') {
                event.preventDefault();
                return;
            }
            const titleId = this.newTitleAlias.titleId;
            const url = `${VUE_APP_API_BASE_URL}/titles/${titleId}/aliases`;
            axios
                .post(url, { alias: this.newTitleAlias.value })
                .then(response => {
                    if (response.status == 200) {
                        this.loadTitleItems();
                    } else {
                        throw Error("Non 200 status code returned from server");
                    }
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        },

        deleteTitleAlias(titleId: number, aliasId: number) {
            const url = `${VUE_APP_API_BASE_URL}/titles/${titleId}/aliases/${aliasId}`;
            axios
                .delete(url)
                .then(response => {
                    if (response.status == 200) {
                        this.loadTitleItems();
                    } else {
                        throw Error("Non 200 status code returned from server");
                    }
                })
                .catch(error => {
                    this.showErrorMessage(error);
                    console.error(error);
                });
        }
    }
});
</script>

<style lang="scss">
.loading {
    display: flex;
    flex-direction: column;

    .loading-item {
        display: flex;
        justify-content: center;
    }
}

.bold-minus {
    font-weight: bold;
    font-size: 1.5em;
}
</style>
