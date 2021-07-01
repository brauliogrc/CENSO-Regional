import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-panelusuariobusq',
  templateUrl: './panelusuariobusq.component.html',
  styleUrls: ['./panelusuariobusq.component.css']
})
export class PanelusuariobusqComponent implements OnInit {

  constructor(
    private router: Router
  ) { }

  ngOnInit(): void {
  }
  crearfolio() {
    this.router.navigate(['/panelusuario']);
  }
}
