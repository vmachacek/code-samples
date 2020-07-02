## PromoCode check

![image](https://media1.giphy.com/media/KHEIh5DHaA5cGut55w/giphy.gif)

Here I used RxJs to achieve interesting user experience.
Goal was to avoid invalid state when user have to manually delete promocode code. 

```
private promoCodeSubject = new Subject<string>();
private removingSubject = new Subject();

 ngOnInit() {
    //load reservation from resolver
    this.reservation = this.route.snapshot.data.reservation;

    //set title of the page
    this._titleService.setTitle(`Payment method`);

    this.promoCodeSubscription = this.promoCodeSubject
      .asObservable()
      .pipe(debounceTime(1000)) //hit server only after users stops typing
      .subscribe(value => {
        this.busy = true; // let user know we are doing something
        this._promoCodeService
          .validate(value)
          .pipe(finalize(() => (this.busy = false))) // stop loading
          .subscribe(f => {
              this.reservation.promoCode = f; // success
          }, _ => { // error
              // to un-block the form so user can move forward
              this.promoCodeEditor.control.markAsTouched();
              this.promoCodeEditor.control.setErrors({ promocode: { valid: false } });
              this.reservation.promoCode = null;
              this.shouldClearPromoCode = true;
              this.removingSubject.next(); // start countdown to remove code 
          });
      });

    this.removingSubscription = this.removingSubject
      .asObservable()
      .pipe(debounceTime(1500))
      .subscribe(() => {
        if (this.shouldClearPromoCode) {
          this.promoCodeValue = ''; // remove wrong code for a user 
        }
      });
  }
  
```
