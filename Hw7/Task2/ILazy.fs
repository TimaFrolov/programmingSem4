module Hw7.Task2.ILazy

type 'a ILazy =
    abstract member Get: unit -> 'a
