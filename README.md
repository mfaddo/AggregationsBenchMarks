# Linq BenchMarks

In this implementation I did a practical expirement to make compare between LINQ aggregations methods , For , FroEach , and maunual iteration. 

The motivaition behind this experiment was reading below Quote on stackoverflow: <br/>
'Most of the times, LINQ will be a bit slower because it introduces overhead. <br/>
 Do not use LINQ if you care much about performance.<br/>
 Use LINQ because you want shorter better readable and maintainable code.'


# The compared components 
<li>For Loop</li>
<li>For Each</li>
<li>Select</li>
<li>Join</li>
<li>Query Join</li>
<li>Select from Dictionary</li>
<li>Manual Iteration</li>


## Expirement Result 
As per result that you can find in this link : https://github.com/mfaddo/AggregationsBenchMarks/blob/master/AggregationsBenchMarks/Result.md <br/>
Manula Iteration is the fastest in term of speed , however the code is not readable nor simple which will add overhead on code scalability and maintenance. 

so on most of the cases we will use LINQ functions unless you care about the performance tooooo much. 
