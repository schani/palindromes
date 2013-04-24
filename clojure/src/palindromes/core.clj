(ns palindromes.core)

(defn ^:static revnum ^long [^long i]
  (loop [i i
         r 0]
    (if (zero? i)
      r
      (let [d (rem i 10)
            i (quot i 10)
            r (+ (* r 10) d)]
        (recur i r)))))

(defn ^:static palindrome? [^long i]
  (== i (revnum i)))

(defn count-palindromes [lower upper]
  (let [lower-sqrt (long (Math/sqrt lower))
        upper-sqrt (long (Math/sqrt upper))]
    (loop [i lower-sqrt
           cnt 0]
      (if (> i upper-sqrt)
        cnt
        (if (and (palindrome? i)
                 (let [sqr (* i i)]
                   (and (palindrome? sqr)
                        (>= sqr lower)
                        (<= sqr upper))))
          (recur (inc i) (inc cnt))
          (recur (inc i) cnt))))))

(defn -main [& args]
  (dotimes [i (read)]
    (let [lower (read)
          upper (read)
          cnt (count-palindromes lower upper)]
      (printf "Case #%d: %d\n" (inc i) cnt)
      (flush))))
