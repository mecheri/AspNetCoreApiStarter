import { Component, AfterViewInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { FormControl, Validators } from '@angular/forms';

@Component({
  selector: 'app-widgets',
  templateUrl: './widgets.component.html',
  styleUrls: ['./widgets.component.scss']
})
export class WidgetsComponent implements AfterViewInit {
  // Timeline
  mytimelines: any[] = [
    {
      from: 'Nirav joshi',
      time: '(5 minute ago)',
      image: 'assets/images/users/1.jpg',
      attachment: 'assets/images/big/img2.jpg',
      content:
        // tslint:disable-next-line:max-line-length
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper'
    },
    {
      from: 'Sunil joshi',
      time: '(3 minute ago)',
      image: 'assets/images/users/2.jpg',
      content:
        // tslint:disable-next-line:max-line-length
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper',
      buttons: 'primary'
    },
    {
      from: 'Vishal Bhatt',
      time: '(1 minute ago)',
      image: 'assets/images/users/3.jpg',
      attachment: 'assets/images/big/img1.jpg',
      content:
        // tslint:disable-next-line:max-line-length
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper'
    },
    {
      from: 'Dhiren Adesara',
      time: '(1 minute ago)',
      image: 'assets/images/users/4.jpg',
      content:
        // tslint:disable-next-line:max-line-length
        'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Integer nec odio. Praesent libero. Sed cursus ante dapibus diam. Sed nisi. Nulla quis sem at nibh elementum imperdiet. Duis sagittis ipsum. Praesent mauris. Fusce nec tellus sed augue semper',
      buttons: 'warn'
    }
  ];

  ngAfterViewInit() {
    const sparklineLogin = function() {
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
      // site A
      (<any>$('.sitea')).sparkline([2, 4, 4, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
        type: 'line',
        width: '90%',
        height: '50',
        lineColor: '#26c6da',
        fillColor: '#26c6da',
        maxSpotColor: '#26c6da',
        highlightLineColor: 'rgba(0, 0, 0, 0.2)',
        highlightSpotColor: '#26c6da'
      });
      // site B
      (<any>$('.siteb')).sparkline([2, 4, 4, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
        type: 'line',
        width: '90%',
        height: '50',
        lineColor: '#1e88e5',
        fillColor: '#1e88e5',
        maxSpotColor: '#1e88e5',
        highlightLineColor: 'rgba(0, 0, 0, 0.2)',
        highlightSpotColor: '#1e88e5'
      });
      // site C
      (<any>$('.sitec')).sparkline([2, 4, 4, 6, 8, 5, 6, 4, 8, 6, 6, 2], {
        type: 'line',
        width: '90%',
        height: '50',
        lineColor: '#f86c6b',
        fillColor: '#f86c6b',
        maxSpotColor: '#f86c6b',
        highlightLineColor: 'rgba(0, 0, 0, 0.2)',
        highlightSpotColor: '#f86c6b'
      });
    };
    let sparkResize;
    (<any>$(window)).resize(function(e) {
      clearTimeout(sparkResize);
      sparkResize = setTimeout(sparklineLogin, 500);
    });
    sparklineLogin();
  }
}
