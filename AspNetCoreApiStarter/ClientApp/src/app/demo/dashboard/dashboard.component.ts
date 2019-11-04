import { Component, AfterViewInit } from '@angular/core';
import * as Chartist from 'chartist';
import { ChartType, ChartEvent } from 'ng-chartist/dist/chartist.component';
declare var require: any;

const data: any = require('./data.json');

export interface Chart {
  type: ChartType;
  data: Chartist.IChartistData;
  options?: any;
  responsiveOptions?: any;
  events?: ChartEvent;
}

@Component({
  selector: 'app-dashboard',
  templateUrl: './dashboard.component.html',
  styleUrls: ['./dashboard.component.scss']
})
export class DashboardComponent implements AfterViewInit {
  // Barchart
  barChart1: Chart = {
    type: 'Bar',
    data: data['Bar'],
    options: {
      seriesBarDistance: 15,
      high: 12,

      axisX: {
        showGrid: false,
        offset: 20
      },
      axisY: {
        showGrid: true,
        offset: 40
      }
    },

    responsiveOptions: [
      [
        'screen and (min-width: 640px)',
        {
          axisX: {
            labelInterpolationFnc: function (
              value: number,
              index: number
            ): string {
              return index % 1 === 0 ? `${value}` : null;
            }
          }
        }
      ]
    ]
  };

  // This is for the donute chart
  donuteChart1: Chart = {
    type: 'Pie',
    data: data['Pie'],
    options: {
      donut: true,
      showLabel: false,
      donutWidth: 30
    }
    // events: {
    //   draw(data: any): boolean {
    //     return data;
    //   }
    // }
  };
  // This is for the line chart
  // Line chart
  lineChart1: Chart = {
    type: 'Line',
    data: data['LineWithArea'],
    options: {
      low: 0,
      high: 35000,
      showArea: true,
      fullWidth: true
    }
  };

  // Timeline
  mytimelines: any[] = [
    {
      from: 'Nirav joshi',
      time: '(5 minute ago)',
      image: 'assets/images/users/1.jpg',
      attachment: 'assets/images/big/img2.jpg',
      content:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero.'
    },
    {
      from: 'Sunil joshi',
      time: '(3 minute ago)',
      image: 'assets/images/users/2.jpg',
      content:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero.',
      buttons: 'primary'
    },
    {
      from: 'Vishal Bhatt',
      time: '(1 minute ago)',
      image: 'assets/images/users/3.jpg',
      attachment: 'assets/images/big/img1.jpg',
      content:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero.'
    },
    {
      from: 'Dhiren Adesara',
      time: '(1 minute ago)',
      image: 'assets/images/users/4.jpg',
      content:
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero.',
      buttons: 'warn'
    }
  ];

  ngAfterViewInit() {
    // Sparkline chart
    const sparklineLogin = function () {
      // spark count
      (<any>$('.spark-count')).sparkline(
        [4, 5, 0, 10, 9, 12, 4, 9, 4, 5, 3, 10, 9, 12, 10, 9, 12, 4, 9],
        {
          type: 'bar',
          width: '100%',
          height: '70',
          barWidth: '2',
          resize: true,
          barSpacing: '6',
          barColor: 'rgba(255, 255, 255, 0.3)'
        }
      );
    };
    let sparkResize;
    (<any>$(window)).resize(function (e) {
      clearTimeout(sparkResize);
      sparkResize = setTimeout(sparklineLogin, 500);
    });
    sparklineLogin();
  }
}
