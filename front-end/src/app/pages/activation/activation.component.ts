import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-activation',
  imports: [],
  templateUrl: './activation.component.html',
  styleUrl: './activation.component.scss'
})
export class ActivationComponent implements OnInit {
  constructor(private route: ActivatedRoute) {}

    ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('guid');
    console.log('ID recebido pela URL:', id);
  }
}
