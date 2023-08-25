import { FormControl, FormGroupDirective, NgForm } from "@angular/forms";
import { ErrorStateMatcher } from "@angular/material/core";

export class ConfirmarErrorStateMatcher implements ErrorStateMatcher {
  isErrorState(control: FormControl | null, form: FormGroupDirective | NgForm | null): boolean  {
    const invalidCtrl = !!(control?.invalid && control?.touched);
    const invalidParent = !!(control?.parent?.get('email')?.invalid && control?.parent?.get('email')?.touched);
    return ( invalidCtrl || invalidParent );
  }
}
