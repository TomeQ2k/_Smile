import { Component, Input, OnInit } from '@angular/core';
import { Group } from 'src/app/models/domain/group/group';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-group-details',
  templateUrl: './group-details.component.html',
  styleUrls: ['./group-details.component.scss']
})
export class GroupDetailsComponent implements OnInit {
  @Input() group: Group;

  currentUserId: string;

  constructor(private authService: AuthService) { }

  ngOnInit(): void {
    this.currentUserId = this.authService.currentUser?.id;
  }
}
