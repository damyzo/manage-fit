import { Component, OnInit } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';
import { HttpClient } from '@angular/common/http';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatSlideToggleModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent implements OnInit {
  title = 'manage-fit';
  constructor(public http: HttpClient){}

  ngOnInit(): void {
  }
  toggle(){
    this.http.get('https://localhost:7003/api/clients').subscribe((data) => {console.log(data)});
    console.log("sadas");
  }
}
