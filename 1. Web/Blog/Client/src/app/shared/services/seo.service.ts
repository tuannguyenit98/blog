import { Injectable } from '@angular/core';
import { Title } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root',
})
export class SeoService {
  constructor(private titleService: Title) {}
  setPageTitle(slug: string): void {
    const formattedTitle = this.formatSlugForTitle(slug);
    this.titleService.setTitle(formattedTitle);
  }

  private formatSlugForTitle(slug: string): string {
    let title = slug.replace(/-/g, ' ');
    title = title
      .split(' ')
      .map((word) => word.charAt(0).toUpperCase() + word.slice(1))
      .join(' ');

    return title;
  }
}
