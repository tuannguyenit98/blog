import { Component } from '@angular/core';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
})
export class HeaderComponent {
  searchVisible: boolean = false;
  quickViewVisible: boolean = false;
  isFolded: boolean = false;
  isExpand: boolean = false;

  constructor(private authService: AuthService) {}

  ngOnInit(): void {}

  toggleFold() {
    this.isFolded = !this.isFolded;
  }

  toggleExpand() {
    this.isFolded = false;
    this.isExpand = !this.isExpand;
  }

  searchToggle(): void {
    this.searchVisible = !this.searchVisible;
  }

  quickViewToggle(): void {
    this.quickViewVisible = !this.quickViewVisible;
  }

  logout(): void {
    this.authService.logut().subscribe();
  }
}
