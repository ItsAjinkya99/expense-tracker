import { Component, ChangeDetectionStrategy } from '@angular/core';

@Component({
  selector: 'app-expense',
  imports: [],
  changeDetection: ChangeDetectionStrategy.Eager,
  template: `
    <p>
      expense works!
    </p>
  `,
})
export class Expense {

}
