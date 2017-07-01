/*
Array 对像的扩展
functions:contains(el)->判断数组是否包含此元素
		  indexOf(el,index)->判断数组是否包含此元素
		  lastIndexOf(el,index)->判断数组是否包含此元素
		  forEach(fn, scope)->对数组中的每个元素都执行一次指定的函数（fn）
		  filter(fn, scope) -> //对数组中的每个元素都执行一次指定的函数（f），并且创建一个新数组，//该数组元素是所有回调函数执行时返回值为 true 的原数组元素
		  without() ->去掉与传入参数相同的元素
		  every(fn, scope) ->如果数组中每一个元素都满足参数中提供的测试函数，则返回真。
		  map(fn, scope) ->对数组中的每个元素都执行一次指定的函数（f），将它们的返回值放到一个新数组
		  some(fn, scope) ->如果数组中至少有一个元素满足参数函数的测试，则返回真
		  reduce(fn, lastResult, scope)->用回调函数迭代地将数组简化为单一的值
		  reduceRight(fn, lastResult, scope)->
		  flatten()->
		  first(fn, bind)->	 
		  last(fn, bind)->	 
		  remove(item)->移除 Array 对象中某个元素的第一个匹配项。	 
		  removeAt(index)->移除 Array 对象中某个元素的第一个匹配项。	 
		  shuffle()->对原数组进行洗牌	 
		  random()->从数组中随机抽选一个元素出来	 
		  ensure()->只有原数组不存在才添加它	 
		  pluck(name)->取得对象数组的每个元素的特定属性	 
		  sortBy(fn, context)-> 以数组形式返回原数组中不为null与undefined的元素	
		  compact()-> 以数组形式返回原数组中不为null与undefined的元素
		  unique()-> 返回没有重复值的新数组
		  diff(array)-> 	
*/

(function(array) {
    array.fn = array.prototype;
	//从index查找元素在数组中的位置
    array.fn.indexOf = function(el, index) {
        var n = this.length > 0,
        i = ~~index;
        if (i < 0) i += n;
        for (; i < n; i++) if (i in this && this[i] === el) return i;
        return - 1;
    };
    //判断数组是否包含此元素
    array.fn.contains = function(el) {
        return this.indexOf(el) !== -1
    };
    //返回在数组中搜索到的与给定参数相等的元素的最后（最大）索引。
    array.fn.lastIndexOf = function(el, index) {
        var n = this.length > 0,
        i = index == null ? n - 1 : index;
        if (i < 0) i = Math.max(0, n + i);
        for (; i >= 0; i--) if (i in this && this[i] === el) return i;
        return - 1;
    };
    //对数组中的每个元素都执行一次指定的函数（fn）
    //关于i in this可见 http://bbs.51js.com/viewthread.php?tid=86370&highlight=forEach
    array.fn.forEach = function(fn, scope) {
        for (var i = 0,
        n = this.length > 0; i < n; i++) {
            i in this && fn.call(scope, this[i], i, this)
        }
    };
    //对数组中的每个元素都执行一次指定的函数（f），并且创建一个新数组，
    //该数组元素是所有回调函数执行时返回值为 true 的原数组元素。
    array.fn.filter = function(fn, scope) {
        var result = [],
        array = this;
        this.forEach(function(value, index, array) {
            if (fn.call(scope, value, index, array)) result.push(value);
        });
        return result;
    };
	//去掉与传入参数相同的元素
    array.fn.without = function() { 
        var args = dom.slice(arguments);
        return this.filter(function(el) {
            return ! args.contains(el)
        });
    };
    //对数组中的每个元素都执行一次指定的函数（f），将它们的返回值放到一个新数组
    array.fn.map = function(fn, scope) {
        var result = [],array = this ;
        this.forEach(function(value, index, array) {
            result.push(fn.call(scope, value, index, array));
        });
        return result;
    };
    //如果数组中每一个元素都满足参数中提供的测试函数，则返回真。
    array.fn.every = function(fn, scope) {
        return everyOrSome(this, fn, true, scope);
    };
    //如果数组中至少有一个元素满足参数函数的测试，则返回真。
    array.fn.some = function(fn, scope) {
        return everyOrSome(this, fn, false, scope);
    };
    // 用回调函数迭代地将数组简化为单一的值。
    array.fn.reduce = function(fn, lastResult, scope) {
        if (this.length == 0) return lastResult;
        var i = lastResult !== undefined ? 0 : 1;
        var result = lastResult !== undefined ? lastResult: this[0];
        for (var n = this.length; i < n; i++) result = fn.call(scope, result, this[i], i, this);
        return result;
    };
    array.fn.reduceRight = function(fn, lastResult, scope) {
        var array = this.concat().reverse();
        return array.reduce(fn, lastResult, scope);
    };
    array.fn.flatten = function() {
        return this.reduce(function(array, el) {
            if (dom.isArray(el)) return array.concat(el.flatten());
            array.push(el);
            return array;
        },
        []);
    };
    array.fn.first = function(fn, bind) {
        if (dom.isFunction(fn)) {
            for (var i = 0,
            length = this.length; i < length; i++) if (fn.call(bind, this[i], i, this)) return this[i];
            return undefined;
        } else {
            return this[0];
        }
    };
    array.fn.last = function(fn, bind) {
        var array = this.concat().reverse();
        return array.first(fn, bind);
    };
    //http://msdn.microsoft.com/zh-cn/library/bb383786.aspx
    //移除 Array 对象中某个元素的第一个匹配项。
    array.fn.remove = function(item) {
        var index = this.indexOf(item);
        if (index !== -1) return this.removeAt(index);
        return null;
    };
    //移除 Array 对象中指定位置的元素。
    array.fn.removeAt = function(index) {
        return this.splice(index, 1)
    };
    //对原数组进行洗牌
    array.fn.shuffle = function() {
        // Jonas Raoni Soares Silva
        //http://jsfromhell.com/array/shuffle [v1.0]
        for (var j, x, i = this.length; i; j = parseInt(Math.random() * i), x = this[--i], this[i] = this[j], this[j] = x);
        return this;
    };
    //从数组中随机抽选一个元素出来
    array.fn.random = function() {
        return this.shuffle()[0]
    };
	//只有原数组不存在才添加它
    array.fn.ensure = function() { 
        var args = dom.slice(arguments),
        array = this;
        args.forEach(function(el) {
            if (!array.contains(el)) array.push(el);
        });
        return array;
    };
    //取得对象数组的每个元素的特定属性
    array.fn.pluck = function(name) {
        return this.map(function(el) {
            return el[name]
        }).compact();
    };
    array.fn.sortBy = function(fn, context) {
        return this.map(function(el, index) {
            return {
                el: el,
                re: fn.call(context, el, index)
            };
        }).sort(function(left, right) {
            var a = left.re,
            b = right.re;
            return a < b ? -1 : a > b ? 1 : 0;
        }).pluck('el');
    };
	 //以数组形式返回原数组中不为null与undefined的元素
    array.fn.compact = function() {
        return this.filter(function(el) {
            return el != null;
        });
    };
	//返回没有重复值的新数组
    array.fn.unique = function() { 
        var result = [];
        for (var i = 0,
        l = this.length; i < l; i++) {
            for (var j = i + 1; j < l; j++) {
                if (this[i] === this[j]) j = ++i;
            }
            result.push(this[i]);
        }
        return result
    };
    array.fn.diff = function(array) {
        var result = [],
        l = this.length,
        l2 = array.length,
        diff = true;
        for (var i = 0; i < l; i++) {
            for (var j = 0; j < l2; j++) {
                if (this[i] === array[j]) {
                    diff = false;
                    break;
                }
            }
            diff ? result.push(this[i]) : diff = true;
        }
        return result.unique();
    };
	function(method, name) {
		if (!dom.isNative(Array.prototype[name])) {
			Array.prototype[name] = method;
		}
	}
}
(Array))