import { Injectable } from '@angular/core';

export class TranslationSet {
   public languange: string;
   public values: {[key: string]: string} = {};
}

@Injectable()
export class TranslationService {

    public languages = ['ger', 'eng', 'spa'];

    public language = 'spa';

    private dictionary: {[key: string]: TranslationSet} = {
        'ger' : {
            languange: 'ger',
            values: {
                'example' : 'Beispiel'
            }
        },
        'eng' : {
            languange: 'eng',
            values: {
                'example' : 'Example',
                'username' : 'Username',
                'password' : 'Password',
                'incorrectLogin' : 'Usuario/Contraseña wrong',
                'dashboard' : 'Dashboard',
                'blank-page' : 'Test',
                'field-required' : 'This field is required',
                'validate': 'VALIDATE',
                'connect': 'Connect ...',
                'orconnectwith': 'Or connect with ...',
                'allrightsreserved': 'all rights reserved'
            }
        },
        'spa' : {
            languange: 'spa',
            values: {
                'example' : 'Ejemplo',
                'username' : 'Usuario',
                'password' : 'Contraseña',
                'incorrectLogin' : 'Usuario/Contraseña incorrectos',
                'dashboard' : 'Escritorio',
                'blank-page' : 'Test',
                'field-required' : 'Este campo es necesario',
                'validate': 'VALIDAR',
                'connect': 'Conectando ...',
                'orconnectwith': 'O conecta con ...',
                'allrightsreserved': 'todos los derechos reservados',
                'search': 'buscar'
            }
        }
    };

    constructor() { }

    translate(value: string): string {
        if ( this.dictionary[this.language] != null) {
            const traduction = this.dictionary[this.language].values[value];
            if (traduction == null || traduction === '' || traduction === undefined) {
                return value;
            } else {
                return traduction;
            }
        }
    }
}
