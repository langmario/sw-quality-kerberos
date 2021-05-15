export interface Salutation {
    id : number
    value : string
    languageKey : string
    gender : Gender
}

enum Gender {
    MALE,
    FEMALE,
    DIVERSE
}

export interface Title {
    id : number
    key : string
    aliasList : TitleAlias[]
}

export interface TitleAlias {
    id : number
    value : string
    title : Title
}