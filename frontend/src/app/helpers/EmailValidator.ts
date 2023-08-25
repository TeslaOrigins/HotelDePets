
import { AbstractControl, ValidationErrors, ValidatorFn } from "@angular/forms";

export function ConfirmacaoEmailValidator(): ValidatorFn {
  return (control: AbstractControl): ValidationErrors | null => {
    return (control?.parent?.get('email')?.value !== control.value) ? { emailDiferente: true } : null
  };
}


