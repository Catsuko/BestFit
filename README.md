# BestFit
Console Application that determines the maximum amount of phones that can be placed in a box.

## Problem

Given a box and a unlimited supply of a smaller phone box, what is the maximum amount of phone boxes we can place without exceeding the bounds of the container box? You are allowed to rotate the boxes however you wish!

## Best Fit on Android

Install the VBF.apk package found in the Visual Best Fit directory, then install onto your phone via adb. You may need to enable developer options in order to install unsigned apks.

## Best Fit in Console

Follow the prompts to specify target sizes for the two boxes, the application will then determine a set of phones to use in order to pack it as much as it can.

### Sample Run

```
Phone Size?
5x7.4x4
Box Size?
32x43x22.1

7.4x4x5 => 32x43x22.1
Fitted with 2 layers of 68

Chosen Arrangements:
     68 x Rectangle (4x5)

Total Phones 136
Remaining Distance from Top: 7.3


5x7.4x4 => 32x43x7.3
Fitted with 1 layers of 44

Chosen Arrangements:
     40 x Rectangle (4x7.4)
     4 x Rectangle (7.4x4)

Total Phones 44
Remaining Distance from Top: 2.3


TOTAL: 180            
```
