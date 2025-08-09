import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { AuthService } from '../../services/auth.service';

@Component({
  selector: 'app-activation',
  imports: [CommonModule],
  templateUrl: './activation.component.html',
  styleUrl: './activation.component.scss'
})
export class ActivationComponent implements OnInit {
  constructor(private route: ActivatedRoute, private router: Router, private authService: AuthService, ) {}
    isSuccess = false;
    ngOnInit(): void {
    const guid = this.route.snapshot.paramMap.get('guid');
    if(guid == null)
      return;
    this.authService.activateAccount(guid).subscribe({
      next: res => {
        this.isSuccess = true;
        setTimeout(() => {this.router.navigate(['/home']);},3000);
      },
      error: err => {
        this.isSuccess = false;
      }
    });
  }
}
