<template>
<section>
<h1 class="mb-5">Language</h1>
    <b-card header="Sprachen hinzufügen">
        <b-form @submit="onSubmit">
            <div class="row">
                <div class="col">
                    <b-form-group id="input-key" label="Sprachschlüssel" label-for="key-input">
                        <b-input id="key-input" v-model="formLanguage.key" type="text" required placeholder="de-DE"></b-input>
                    </b-form-group>
                </div>
                <div class="col">
                    <b-form-group id="input-name" label="Name" label-for="name-input">
                        <b-input id="name-input" v-model="formLanguage.name" type="text" required placeholder="Deutsch"></b-input>
                    </b-form-group>
                </div>
            </div>
            <div class="row">
                <div class="col">
                    <b-button type="submit" variant="success">Sprache hinzufügen</b-button>
                </div>
            </div>
        </b-form>
    </b-card>
    <div class="mt-3">
        <div v-if="loading">
            Lade Daten. Bitte warten...<br><br>
        <b-spinner/>
        </div>
        <b-table v-if="languageItems.length != 0" bordered hover striped :items="languageItems">
        </b-table>
        <p v-if="languageItems.length == 0 && !loading && !error" class="text-center">Keine Daten vorhanden</p>
        <b-card
            v-if="error"
            border-variant="danger"
            header="Es ist ein Fehler aufgetreten"
            header-border-variant="danger"
            header-text-variant="danger"
            align="center">
            <b-card-text>
                {{errorMsg}}
            </b-card-text>
        </b-card>
    </div>
</section>
</template>

<script lang="ts">
import axios from 'axios'
import Vue from 'vue'
import {Language} from '../models/models'
const {VUE_APP_API_BASE_URL} = process.env

export default Vue.component("Language", {
    data() {
        return {
            loading: false,
            languageItems: [] as Language[],
            error: false,
            errorMsg: "Fehler...",
            formLanguage: {} as Language
        }
    },
    mounted() {
        this.loadLangaugeItems();
    },
    methods: {
        loadLangaugeItems(){
            this.loading = true;
            const url = VUE_APP_API_BASE_URL + "/api/languages";
            axios.get(url).then(response => {
                this.languageItems = response.data;
                this.loading = false;
            }).catch(error => {
                console.error(error);
                this.loading = false;
                this.error = true;
                this.errorMsg = error;
            });
        },
        onSubmit(event: any){
            event.preventDefault();
            const url = VUE_APP_API_BASE_URL + "/api/languages";
            console.log(url)
            axios.post(url, this.formLanguage).then(response => {
                this.formLanguage.name = '';
                this.formLanguage.key = '';
                console.log(response)
                const language: Language = response.data
                this.languageItems.push(language)
            }).catch(error => {
                // TODO: error handling
                console.error(error);
            })
        }
    }
})
</script>

<style lang="scss">

</style>