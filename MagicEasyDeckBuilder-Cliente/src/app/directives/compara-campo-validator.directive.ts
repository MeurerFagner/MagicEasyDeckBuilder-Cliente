import { Attribute, Directive } from '@angular/core';
import { AbstractControl, FormControl, NG_VALIDATORS, ValidationErrors, Validator } from '@angular/forms';
import { Subscription } from 'rxjs';

@Directive({
  selector: '[appComparaCampoValidator]',
  providers: [
    {
      provide: NG_VALIDATORS,
      useClass: ComparaCampoValidatorDirective,
      multi: true
    }
  ]
})
export class ComparaCampoValidatorDirective implements Validator {

  constructor(@Attribute('appComparaCampoValidator') public CampoControl: string) { }

  validate(control: FormControl) {
    const campo = control.root.get(this.CampoControl)
    const comfirmaCampo = control;

    if (comfirmaCampo.value === null) {
      return null
    }

    if (campo){
      const subscription: Subscription = campo.valueChanges.subscribe(() =>{
        comfirmaCampo.updateValueAndValidity()
        subscription.unsubscribe()
      })
    }
    return campo && campo.value !== comfirmaCampo.value 
      ?  { comparMatcherror: true } : null;
  }
}
