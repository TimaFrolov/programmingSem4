module Tw1.Task1

let sum_of_even_fibonacci N =
    let rec helper a b sum =
        if a > N then
            sum
        else
            helper (a + b) a (if a % 2I = 0I then sum + a else sum)

    helper 1I 1I 0I

let answer = sum_of_even_fibonacci 1_000_000I

