import { AbstractControl } from '@angular/forms';
export class PasswordValidation {

    static MatchPassword(AC: AbstractControl) {
        let password = AC.get('Password').value; // to get value in input tag
        let confirmPassword = AC.get('PasswordConfirm').value; // to get value in input tag
        if (password != confirmPassword) {
            AC.get('PasswordConfirm').setErrors({ MatchPassword: true })
        } else {
            return null
        }
    }
}