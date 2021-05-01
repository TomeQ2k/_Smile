import { Component, Input } from '@angular/core';
import { Group } from 'src/app/models/domain/group/group';

@Component({
  selector: 'app-group-dashboard',
  templateUrl: './group-dashboard.component.html',
  styleUrls: ['./group-dashboard.component.scss']
})
export class GroupDashboardComponent {
  @Input() group: Group;
}
