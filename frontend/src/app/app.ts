import { Component, signal, ChangeDetectionStrategy } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { HlmButton, HlmButtonImports } from '@spartan-ng/helm/button';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, HlmButtonImports],
  templateUrl: './app.html',
  changeDetection: ChangeDetectionStrategy.Eager,
  styleUrl: './app.css'
})
export class App {
  protected readonly title = signal('angular-expense-tracker');
}
