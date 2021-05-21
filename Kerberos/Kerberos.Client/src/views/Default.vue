<template>
    <div class="home">
        <h1 class="mb-5">Eingabe</h1>
        <b-card header="Anrede eingeben">
            <b-form @submit="onSubmit">
                <b-form-group
                    id="input-key"
                    label="Anrede"
                    label-for="key-input"
                >
                    <b-input
                        type="text"
                        v-model="input"
                        required
                        placeholder="Herr Max Mustermann"
                    ></b-input>
                </b-form-group>
                <b-button variant="success" type="submit">Senden</b-button>
                <b-button variant="danger" @click="reset" class="ml-2"
                    >Reset</b-button
                >
            </b-form>
        </b-card>
        <b-row class="mt-4" v-if="wasParsed">
            <b-col>
                <b-card header="Anrede">
                    <table class="table" v-if="salutation">
                        <tr>
                            <td>Anrede</td>
                            <th>{{ salutation ? salutation.value : "" }}</th>
                        </tr>
                        <tr>
                            <td>Sprache</td>
                            <th>
                                {{ salutation ? salutation.language.name : "" }}
                            </th>
                        </tr>
                        <tr>
                            <td>Geschlecht</td>
                            <th>
                                {{
                                    salutation
                                        ? getGenderName(salutation.gender)
                                        : ""
                                }}
                            </th>
                        </tr>
                    </table>
                </b-card>
            </b-col>
            <b-col>
                <b-card header="Titel">
                    <b-form-checkbox
                        v-for="title in titles"
                        :key="title.id"
                        :value="title.value"
                        v-model="selectedTitles"
                        >{{ title.value }} <span class="small muted"> {{title.aliases[0] ? `(${title.aliases[0].value})` : ''}}</span></b-form-checkbox
                    >
                </b-card>
                <b-card class="mt-2" header="Komma">
                    <b-form-checkbox v-model="comma"
                        >Komma am Ende der Anrede</b-form-checkbox
                    >
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
            <div class="pos">
                <h4>{{ completeMailSalutation }}</h4>
                <svg
                    v-b-tooltip.click
                    title="Briefanrede kopiert"
                    v-clipboard="() => completeMailSalutation"
                    xmlns="http://www.w3.org/2000/svg"
                    height="24px"
                    viewBox="0 0 24 24"
                    width="24px"
                    fill="#000000"
                    @click="closeTooltip"
                >
                    <path d="M0 0h24v24H0z" fill="none" />
                    <path
                        d="M16 1H4c-1.1 0-2 .9-2 2v14h2V3h12V1zm3 4H8c-1.1 0-2 .9-2 2v14c0 1.1.9 2 2 2h11c1.1 0 2-.9 2-2V7c0-1.1-.9-2-2-2zm0 16H8V7h11v14z"
                    />
                </svg>
            </div>
        </b-card>
    </div>
</template>

<script lang="ts">
import Vue from "vue";
import axios from "axios";
import Clipboard from "v-clipboard";
import { Salutation, Title } from "@/models/models";

Vue.use(Clipboard);

const { VUE_APP_API_BASE_URL } = process.env;

export default Vue.extend({
    name: "Default",
    components: {},
    data() {
        return {
            input: "",
            wasParsed: false,
            firstname: null as string | null,
            lastname: null as string | null,
            titles: null as Title[] | null,
            salutation: null as Salutation | null,
            selectedTitles: [] as Title[],
            comma: true
        };
    },
    methods: {
        async onSubmit(event: any) {
            event.preventDefault();
            const url = VUE_APP_API_BASE_URL + "/parse";
            const result = await axios.post(url, {
                input: this.input
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
                    return "Männlich";
                case 1:
                    return "Weiblich";
                case 2:
                    return "Divers";
                default:
                    return "";
            }
        },

        reset() {
            this.wasParsed = false;
            this.firstname = null;
            this.lastname = null;
            this.titles = null;
            this.salutation = null;
        },

        closeTooltip(event: any) {
            setTimeout(
                (handler: any) => this.$root.$emit("bv::hide::tooltip"),
                2000
            );
        }
    },
    computed: {
        completeMailSalutation: function(): string {
            return `${this.salutation ? this.salutation.formalSalutation : ""}${
                this.selectedTitles.length == 0 ? "" : " "
            }${this.selectedTitles.join(" ")} ${this.firstname} ${
                this.lastname
            }${this.comma ? "," : ""}`;
        }
    }
});
</script>

<style lang="scss">
.pos {
    position: relative;

    svg {
        position: absolute;
        top: 0.5em;
        right: 0.5em;
        fill: grey;
    }

    svg:hover {
        cursor: pointer;
        fill: black;
    }
}

.small {
	font-size: .75em;
}

.muted {
	color: grey;
}
</style>
