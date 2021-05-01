import { Component, OnInit } from '@angular/core';
import { Tile } from 'src/app/models/helpers/tile';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-admin-panel',
  templateUrl: './admin-panel.component.html',
  styleUrls: ['./admin-panel.component.scss']
})
export class AdminPanelComponent implements OnInit {
  tiles: Tile[] = [
    {
      imageUrl: 'fa fa-users',
      title: 'Users',
      routerLink: ['/admin/panel/users']
    },
    {
      imageUrl: 'fas fa-list-ol',
      title: 'Logs',
      routerLink: ['/admin/panel/logs']
    }
  ];

  currentUsername: string;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUsername = this.authService.currentUser.username;
  }
}
