import { Component, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { PostFormComponent } from '../post-form/post-form.component';

@Component({
  selector: 'app-post-edit',
  templateUrl: './post-edit.component.html',
  styleUrls: ['./post-edit.component.scss']
})
export class PostEditComponent implements OnInit {
  @ViewChild('form')
  form!: PostFormComponent;
  id: number = 0;
  constructor(
    private route: ActivatedRoute,
  ){

  }
  ngOnInit(): void {
    this.id = this.route.snapshot.params['id'];
  }
}
