import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Group } from 'src/app/models/domain/group/group';

@Component({
  selector: 'app-group',
  templateUrl: './group.component.html',
  styleUrls: ['./group.component.scss']
})
export class GroupComponent implements OnInit {
  group: Group;

  constructor(private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.subscribeData();
  }

  public openBackgroundImage() {
    const imageWindow = window.open(this.group.imageUrl, '_blank');
    imageWindow.focus();
  }

  private subscribeData = () => {
    this.route.data.subscribe(data => this.group = data.groupResponse.group);
  }
}
