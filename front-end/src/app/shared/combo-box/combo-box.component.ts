import { CommonModule } from '@angular/common';
import { Component, EventEmitter, forwardRef, Input, OnDestroy, OnInit, Output } from '@angular/core';
import { ControlValueAccessor, FormsModule, NG_VALUE_ACCESSOR } from '@angular/forms';
import { Observable, Subscription } from 'rxjs';

@Component({
  selector: 'app-combo-box',
  imports: [CommonModule, FormsModule],
  templateUrl: './combo-box.component.html',
  styleUrl: './combo-box.component.scss',
    providers: [
    {
      provide: NG_VALUE_ACCESSOR,
      useExisting: forwardRef(() => ComboBoxComponent),
      multi: true
    }
  ]
})
export class ComboBoxComponent<T> implements ControlValueAccessor, OnInit, OnDestroy {
  @Input() bindValue!: keyof T;
  @Input() bindLabel!: keyof T;
  @Input() placeholder = 'Selecione...';
  @Output() valueChange = new EventEmitter<T | null>();
  @Input() data$!: Observable<T[]>;
  private subscription!: Subscription;
  items: T[] = [];
  value: any = '';
  disabled = false;
  onChange = (_: any) => {};
  onTouched = () => {};
  ngOnDestroy(): void {
    this.subscription?.unsubscribe();
  }
  ngOnInit(): void {
    this.subscription = this.data$.subscribe(data => {
       const novoItem: T = {[this.bindValue]: '',
                            [this.bindLabel]: this.placeholder} as T;
       this.items = [];
       this.items.push(novoItem);
       data.forEach(item =>{ this.items.push(item)});
       setTimeout(() => this.forcarSelecao(''));

    });
  }
  writeValue(obj: any): void {
    this.value = obj ?? '';
  }
  registerOnChange(fn: any): void {
    this.onChange = fn;
  }
  registerOnTouched(fn: any): void {
    this.onTouched = fn;
  }
  setDisabledState?(isDisabled: boolean): void {
    this.disabled = isDisabled;
  }
  onValueChange(event: Event) {
    const selectedValue = (event.target as HTMLSelectElement).value;
    this.value = selectedValue;
    this.onChange(selectedValue);
    this.onTouched();
    this.valueChange.emit(this.items.find(i => i[this.bindValue] == selectedValue) ?? null);
  }
    forcarSelecao(valor: any): void {

  setTimeout(() => {
    this.value = valor ?? '';
    this.onChange(this.value);
    this.onTouched();
  });
    }

}
