import {Component, OnInit, ViewChild, ElementRef, ViewEncapsulation} from '@angular/core';
import {
  NgForm,
  FormGroup,
  FormControl
} from '@angular/forms';
import { Router } from '@angular/router';
import { FormValidators } from '@syncfusion/ej2-angular-inputs';
import { ToastComponent } from '@syncfusion/ej2-angular-notifications';

// Services
import { AuthenticationService } from '../../shared/services/authentication.service';
import { StorageService } from '../../shared/services/storage.service';
import { TranslationService } from '../../shared/services/translation.service';
import { WaitService } from '../../shared/services/wait.service';

// Models
import { Login } from '../../shared/models/login.model';
import { Session } from '../../shared/models/session.model';

@Component({
  selector: 'app-login-page',
  templateUrl: './login-page.component.html',
  styleUrls: ['./login-page.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class LoginPageComponent implements OnInit {

  public loginForm: FormGroup;
  public submitted: Boolean = false;
  public errorInLogin: Boolean = false;

  @ViewChild('toastError')
  public toastObj: ToastComponent;

  @ViewChild('spin') spin: ElementRef;

  constructor(
    private authenticationService: AuthenticationService,
    private storageService: StorageService,
    private translationService: TranslationService,
    private waitService: WaitService,
    private router: Router) {
      this.loginForm = new FormGroup({
        'username': new FormControl('', [FormValidators.required]),
        'password': new FormControl('', [FormValidators.required])
      });
  }

  ngOnInit() {
    document.getElementById('formId').addEventListener(
      'submit',
      (e: Event) => {
        e.preventDefault();
        if (!this.loginForm.valid) {
          Object.keys(this.loginForm.controls).forEach(field => {
            const control = this.loginForm.get(field);
            control.markAsTouched({ onlySelf: true });
          });
        }
    });

    this.waitService.createSpinner({
      target: this.spin.nativeElement,
      // label: this.translationService.translate('connect')
    });
  }

  public submitLogin(form: NgForm): void {
    this.submitted = true;
    localStorage.setItem('isLoggedin', 'true');
    if (this.loginForm.valid) {
      this.waitService.showSpinner(this.spin.nativeElement);
      this.authenticationService.login(new Login(this.loginForm.value)).subscribe(
        data => {
          this.waitService.hideSpinner(this.spin.nativeElement);
          this.correctLogin(data);
        },
        error => {
          this.showError(error.messageError);
          this.waitService.hideSpinner(this.spin.nativeElement);
        });
    } else {
      this.showError('Formulario incorrecto');
    }
  }

  private correctLogin(data: Session) {
    this.storageService.setCurrentSession(data);
    localStorage.setItem('isLoggedin', 'true');
    this.router.navigate(['/dashboard']);
  }

  private showError(error: string) {
    this.toastObj.width = '100%';
    this.toastObj.position.X = 'Center';
    this.toastObj.position.Y = 'Bottom';
    this.toastObj.show(
      {
        title: 'Error',
        content: error,
        // cssClass: 'e-toast-success',
        // icon: 'e-success toast-icons',
        cssClass: 'e-toast-danger',
        icon: 'e-error toast-icons'
      }
    );
  }

  get username() {
    return this.loginForm.get('username');
  }

  get password() {
    return this.loginForm.get('password');
  }
}
