import { Injectable } from '@angular/core';
import {
  createSpinner,
  showSpinner,
  setSpinner,
  hideSpinner, SpinnerArgs, SetSpinnerArgs, SpinnerType
} from '@syncfusion/ej2-popups';

@Injectable()

export class WaitService {

  constructor() {
  }

  hideSpinner: Function = (element: HTMLElement) => {
    hideSpinner(element);
  }

  showSpinner: Function = (element: HTMLElement) => {
    showSpinner(element);
  }

  createSpinner: Function = (spinnerArgs: SpinnerArgs) => {
    createSpinner({
      target: spinnerArgs.target,
      label: spinnerArgs.label,
      cssClass: spinnerArgs.cssClass,
      template: spinnerArgs.template,
      type: spinnerArgs.type,
      width: spinnerArgs.width,
    });
  }

  setSpinner: Function = (spinnerArgs: SetSpinnerArgs) => {
    setSpinner({
      cssClass: spinnerArgs.cssClass,
      template: spinnerArgs.template,
      type: spinnerArgs.type,
    });
  }
}
