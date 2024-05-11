import { Component, ViewChild } from '@angular/core';
import { PostFormComponent } from '../post-form/post-form.component';

@Component({
  selector: 'app-post-create',
  templateUrl: './post-create.component.html',
  styleUrls: ['./post-create.component.scss']
})
export class PostCreateComponent {
  @ViewChild('form')
  form!: PostFormComponent;
}
