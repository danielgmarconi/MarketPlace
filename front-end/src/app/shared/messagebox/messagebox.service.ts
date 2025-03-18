import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { IconType } from './icon-type';

@Injectable({
  providedIn: 'root'
})

export class MessageboxService {


  constructor() { }
  private modalSubject = new BehaviorSubject<{ isVisible: boolean, title: string, description: string, icon: string }>({
    isVisible: false,
    title: '',
    description: '',
    icon: IconType.default
  });
  modalState$ = this.modalSubject.asObservable();

  openModal(title: string, description: string, icon:IconType) {

    this.modalSubject.next({ isVisible: true, title, description, icon });
  }

  closeModal() {
    this.modalSubject.next({ isVisible: false, title: '', description: '', icon: '' });
  }
}
