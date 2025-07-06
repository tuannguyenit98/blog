import { Component } from '@angular/core';

@Component({
  selector: 'app-contact',
  templateUrl: './contact.component.html',
  styleUrls: ['./contact.component.scss']
})
export class ContactComponent {
skillListData = ['.Net', 'Angular', 'C#', 'TypeScript', 'HTML', 'CSS', 'SQL', 'Git'];

    expListData = [
        {
            img: 'assets/images/others/adobe-thumb.png',
            title: '.NET Developer',
            company: 'Glotech JSC',
            date: 'Sep 2021 - Present'
        },
        {
            img: 'assets/images/others/amazon-thumb.png',
            title: 'C# Developer',
            company: 'BYS JSC',
            date: 'Jul-2020 - Jan 2021'
        }
    ]

    eduListData = [
        {
            img: 'assets/images/others/cambridge-thumb.png',
            degree: 'Information Technology',
            school: 'Da Nang University Of Education And Science',
            date: 'Sep-2016 - Nov 2020'
        },  
    ]
}
