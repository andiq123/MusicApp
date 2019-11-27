import { Component, OnInit, Output } from '@angular/core';
import { Subject } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.css']
})
export class HeaderComponent implements OnInit {
  @Output() sidenavToggle = new Subject<void>();
  constructor() {}

  ngOnInit() {}
  onSidenavToggle() {
    this.sidenavToggle.next();
  }
}
