import { Component } from '@angular/core';
import { MessageboxService } from '../../shared/messagebox/messagebox.service';
import { IconType } from '../../shared/messagebox/icon-type';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [],
  templateUrl: './home.component.html',
  styleUrl: './home.component.scss'
})
export class HomeComponent {

  constructor(private messageboxService : MessageboxService){}
  teste()
  {
    this.messageboxService.openModal('a', 'b', IconType.danger);
  }
}
