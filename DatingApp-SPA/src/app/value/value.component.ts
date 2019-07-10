import { Component, OnInit, OnDestroy } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Subscription } from 'rxjs';

@Component({
  selector: 'app-value',
  templateUrl: './value.component.html',
  styleUrls: ['./value.component.css']
})
export class ValueComponent implements OnInit, OnDestroy {

  values: any;
  getValuesSubscription: Subscription;
  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getValues();
  }

  ngOnDestroy() {
    this.getValuesSubscription.unsubscribe();
  }

  getValues() {
    this.getValuesSubscription = this.http.get('http://localhost:5000/api/values')
    .subscribe(response => {
      this.values = response;
    }, error => {
      console.log(error);
    });
  }

}
