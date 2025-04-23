import { Component, inject } from '@angular/core';
import { LoadingService } from '../../../../core/services/loading.service';

@Component({
  selector: 'app-page-spinner',
  standalone: true,
  template: `
    @if (loadingService.isLoading()) {
    <div class="spinner-overlay">
      <div class="spinner"></div>
    </div>
    }
  `,
  styleUrl: './page-spinner.component.css',
})
export class PageSpinnerComponent {
  readonly loadingService = inject(LoadingService);
}
