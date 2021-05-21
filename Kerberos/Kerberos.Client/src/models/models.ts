// Definition of typescript models according to api contract
export interface Salutation {
    id : number
    value : string
    language : Language
    formalSalutation: string
    gender : Gender
}

export enum Gender {
    MALE,
    FEMALE,
    DIVERSE
}

export interface Title {
    id : number
    value : string
    aliasList : TitleAlias[]
}

export interface TitleAlias {
    id : number
    value : string
}

export interface Language {
    id : number
    key : string
    name : string
}