import { Routes } from '@angular/router';

import { AutocompleteComponent } from './autocomplete/autocomplete.component';
import { CheckboxComponent } from './checkbox/checkbox.component';
import { RadiobuttonComponent } from './radiobutton/radiobutton.component';
import { FormfieldComponent } from './formfield/formfield.component';
import { DatepickerComponent } from './datepicker/datepicker.component';
import { FormLayoutComponent } from './form-layouts/form-layout.component';
import { PaginatiorComponent } from './paginator/paginator.component';
import { SortheaderComponent } from './sortheader/sortheader.component';
import { SelectfieldComponent } from './select/select.component';
import { InputfieldComponent } from './input/input.component';
import { TreeComponent } from './tree/tree.component';
import { EditorComponent } from './editor/editor.component';
import { FormValidationComponent } from './form-validation/form-validation.component';
import { UploadComponent } from './file-upload/upload.component';
import { WizardComponent } from './wizard/wizard.component';
import { MultiselectComponent } from './multiselect/multiselect.component';

export const FormRoutes: Routes = [
  {
    path: '',
    children: [
      {
        path: 'autocomplete',
        component: AutocompleteComponent
      },
      {
        path: 'checkbox',
        component: CheckboxComponent
      },
      {
        path: 'radiobutton',
        component: RadiobuttonComponent
      },
      {
        path: 'datepicker',
        component: DatepickerComponent
      },
      {
        path: 'formfield',
        component: FormfieldComponent
      },
      {
        path: 'input',
        component: InputfieldComponent
      },
      {
        path: 'select',
        component: SelectfieldComponent
      },
      {
        path: 'tree',
        component: TreeComponent
      },
      {
        path: 'paginator',
        component: PaginatiorComponent
      },
      {
        path: 'form-layout',
        component: FormLayoutComponent
      },
      {
        path: 'editor',
        component: EditorComponent
      },
      {
        path: 'form-validation',
        component: FormValidationComponent
      },
      {
        path: 'file-upload',
        component: UploadComponent
      },
      {
        path: 'sortheader',
        component: SortheaderComponent
      },
      {
        path: 'wizard',
        component: WizardComponent
      },
      {
        path: 'multiselect',
        component: MultiselectComponent
      }
    ]
  }
];
