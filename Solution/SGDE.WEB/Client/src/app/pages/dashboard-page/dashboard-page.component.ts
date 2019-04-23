import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {ResizeService} from '../../resize/resize.service';

@Component({
  selector: 'app-dashboard-page',
  templateUrl: './dashboard-page.component.html',
  styleUrls: ['./dashboard-page.component.scss'],
  encapsulation: ViewEncapsulation.None
})

export class DashboardPageComponent implements OnInit {

  constructor(private resizeService: ResizeService) {
  }

  ngOnInit() {
  }
}
