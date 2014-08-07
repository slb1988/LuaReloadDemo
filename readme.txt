
这是一个lua不停机热更新的Demo，全局使用一个LuaState，按R键reload所有脚本

1. 对于 新添加的panel 绑定对应的lua脚本，并设置模块名
2. 然后在LuaBindingManager上添加需要执行脚本操作的面板

不同面板之间，可以通过 "模块名.函数名" 的方式进行访问

核心的语句是这个，通过这种方式产生 域名空间 的概念
local modname = "bbb"
local M = {}
_G[modname] = M
package.loaded[modname] = M
setmetatable(M, {__index = _G})
setfenv(1, M)

这是一个雏形，你可以按照这个思路添加到自己的项目框架中

have fun!