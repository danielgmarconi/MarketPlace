import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export class CustomValidatorsFormBuilder {
  static emailRegex(): ValidatorFn {
      return (control: AbstractControl): ValidationErrors | null => {
        const email = control.value;
        const regex = /^[^\s@]+@[^\s@]+\.[^\s@]{2,}$/i;
        return regex.test(email) ? null : { emailInvalido: true };
    };
  }
  static fullName(): ValidatorFn {
    return  (control: AbstractControl): ValidationErrors | null => {
      const value = control.value?.trim();
      if (!value) return null;
      const isValid = /^[A-Za-zÀ-ú]+(\s+[A-Za-zÀ-ú]+)+$/.test(value);
      return isValid ? null : { fullNameInvalido: true };
    };
  }
  static password(): ValidatorFn {
    return (control: AbstractControl): ValidationErrors | null => {
      const value = control.value || '';
      const regex = /^(?=.*[A-Z])(?=.*[a-z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,20}$/;
      if (!value) return null;
      return regex.test(value) ? null : { password: true };
    };
  }
}
