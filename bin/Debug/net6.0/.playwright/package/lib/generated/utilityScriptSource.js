"use strict";

Object.defineProperty(exports, "__esModule", {
  value: true
});
exports.source = void 0;
const source = "\nvar __export = (target, all) => {for (var name in all) target[name] = all[name];};\nvar __toCommonJS = mod => ({ ...mod, __esModule: true });\n// packages/playwright-core/src/server/injected/utilityScript.ts\nvar utilityScript_exports = {};\n__export(utilityScript_exports, {\n  UtilityScript: () => UtilityScript\n});\nmodule.exports = __toCommonJS(utilityScript_exports);\n\n// packages/playwright-core/src/server/isomorphic/utilityScriptSerializers.ts\nfunction source() {\n  function isRegExp(obj) {\n    try {\n      return obj instanceof RegExp || Object.prototype.toString.call(obj) === \"[object RegExp]\";\n    } catch (error) {\n      return false;\n    }\n  }\n  function isDate(obj) {\n    try {\n      return obj instanceof Date || Object.prototype.toString.call(obj) === \"[object Date]\";\n    } catch (error) {\n      return false;\n    }\n  }\n  function isURL(obj) {\n    try {\n      return obj instanceof URL || Object.prototype.toString.call(obj) === \"[object URL]\";\n    } catch (error) {\n      return false;\n    }\n  }\n  function isError(obj) {\n    var _a;\n    try {\n      return obj instanceof Error || obj && ((_a = Object.getPrototypeOf(obj)) == null ? void 0 : _a.name) === \"Error\";\n    } catch (error) {\n      return false;\n    }\n  }\n  function parseEvaluationResultValue2(value, handles = [], refs = /* @__PURE__ */ new Map()) {\n    if (Object.is(value, void 0))\n      return void 0;\n    if (typeof value === \"object\" && value) {\n      if (\"ref\" in value)\n        return refs.get(value.ref);\n      if (\"v\" in value) {\n        if (value.v === \"undefined\")\n          return void 0;\n        if (value.v === \"null\")\n          return null;\n        if (value.v === \"NaN\")\n          return NaN;\n        if (value.v === \"Infinity\")\n          return Infinity;\n        if (value.v === \"-Infinity\")\n          return -Infinity;\n        if (value.v === \"-0\")\n          return -0;\n        return void 0;\n      }\n      if (\"d\" in value)\n        return new Date(value.d);\n      if (\"u\" in value)\n        return new URL(value.u);\n      if (\"r\" in value)\n        return new RegExp(value.r.p, value.r.f);\n      if (\"a\" in value) {\n        const result2 = [];\n        refs.set(value.id, result2);\n        for (const a of value.a)\n          result2.push(parseEvaluationResultValue2(a, handles, refs));\n        return result2;\n      }\n      if (\"o\" in value) {\n        const result2 = {};\n        refs.set(value.id, result2);\n        for (const { k, v } of value.o)\n          result2[k] = parseEvaluationResultValue2(v, handles, refs);\n        return result2;\n      }\n      if (\"h\" in value)\n        return handles[value.h];\n    }\n    return value;\n  }\n  function serializeAsCallArgument2(value, handleSerializer) {\n    return serialize(value, handleSerializer, { visited: /* @__PURE__ */ new Map(), lastId: 0 });\n  }\n  function serialize(value, handleSerializer, visitorInfo) {\n    if (value && typeof value === \"object\") {\n      if (typeof globalThis.Window === \"function\" && value instanceof globalThis.Window)\n        return \"ref: <Window>\";\n      if (typeof globalThis.Document === \"function\" && value instanceof globalThis.Document)\n        return \"ref: <Document>\";\n      if (typeof globalThis.Node === \"function\" && value instanceof globalThis.Node)\n        return \"ref: <Node>\";\n    }\n    return innerSerialize(value, handleSerializer, visitorInfo);\n  }\n  function innerSerialize(value, handleSerializer, visitorInfo) {\n    const result2 = handleSerializer(value);\n    if (\"fallThrough\" in result2)\n      value = result2.fallThrough;\n    else\n      return result2;\n    if (typeof value === \"symbol\")\n      return { v: \"undefined\" };\n    if (Object.is(value, void 0))\n      return { v: \"undefined\" };\n    if (Object.is(value, null))\n      return { v: \"null\" };\n    if (Object.is(value, NaN))\n      return { v: \"NaN\" };\n    if (Object.is(value, Infinity))\n      return { v: \"Infinity\" };\n    if (Object.is(value, -Infinity))\n      return { v: \"-Infinity\" };\n    if (Object.is(value, -0))\n      return { v: \"-0\" };\n    if (typeof value === \"boolean\")\n      return value;\n    if (typeof value === \"number\")\n      return value;\n    if (typeof value === \"string\")\n      return value;\n    if (isError(value)) {\n      const error = value;\n      if (\"captureStackTrace\" in globalThis.Error) {\n        return error.stack || \"\";\n      }\n      return `${error.name}: ${error.message}\n${error.stack}`;\n    }\n    if (isDate(value))\n      return { d: value.toJSON() };\n    if (isURL(value))\n      return { u: value.toJSON() };\n    if (isRegExp(value))\n      return { r: { p: value.source, f: value.flags } };\n    const id = visitorInfo.visited.get(value);\n    if (id)\n      return { ref: id };\n    if (Array.isArray(value)) {\n      const a = [];\n      const id2 = ++visitorInfo.lastId;\n      visitorInfo.visited.set(value, id2);\n      for (let i = 0; i < value.length; ++i)\n        a.push(serialize(value[i], handleSerializer, visitorInfo));\n      return { a, id: id2 };\n    }\n    if (typeof value === \"object\") {\n      const o = [];\n      const id2 = ++visitorInfo.lastId;\n      visitorInfo.visited.set(value, id2);\n      for (const name of Object.keys(value)) {\n        let item;\n        try {\n          item = value[name];\n        } catch (e) {\n          continue;\n        }\n        if (name === \"toJSON\" && typeof item === \"function\")\n          o.push({ k: name, v: { o: [], id: 0 } });\n        else\n          o.push({ k: name, v: serialize(item, handleSerializer, visitorInfo) });\n      }\n      if (o.length === 0 && value.toJSON && typeof value.toJSON === \"function\")\n        return innerSerialize(value.toJSON(), handleSerializer, visitorInfo);\n      return { o, id: id2 };\n    }\n  }\n  return { parseEvaluationResultValue: parseEvaluationResultValue2, serializeAsCallArgument: serializeAsCallArgument2 };\n}\nvar result = source();\nvar parseEvaluationResultValue = result.parseEvaluationResultValue;\nvar serializeAsCallArgument = result.serializeAsCallArgument;\n\n// packages/playwright-core/src/server/injected/utilityScript.ts\nvar UtilityScript = class {\n  constructor() {\n    this.serializeAsCallArgument = serializeAsCallArgument;\n    this.parseEvaluationResultValue = parseEvaluationResultValue;\n  }\n  evaluate(isFunction, returnByValue, exposeUtilityScript, expression, argCount, ...argsAndHandles) {\n    const args = argsAndHandles.slice(0, argCount);\n    const handles = argsAndHandles.slice(argCount);\n    const parameters = args.map((a) => parseEvaluationResultValue(a, handles));\n    if (exposeUtilityScript)\n      parameters.unshift(this);\n    let result2 = globalThis.eval(expression);\n    if (isFunction === true) {\n      result2 = result2(...parameters);\n    } else if (isFunction === false) {\n      result2 = result2;\n    } else {\n      if (typeof result2 === \"function\")\n        result2 = result2(...parameters);\n    }\n    return returnByValue ? this._promiseAwareJsonValueNoThrow(result2) : result2;\n  }\n  jsonValue(returnByValue, value) {\n    if (Object.is(value, void 0))\n      return void 0;\n    return serializeAsCallArgument(value, (value2) => ({ fallThrough: value2 }));\n  }\n  _promiseAwareJsonValueNoThrow(value) {\n    const safeJson = (value2) => {\n      try {\n        return this.jsonValue(true, value2);\n      } catch (e) {\n        return void 0;\n      }\n    };\n    if (value && typeof value === \"object\" && typeof value.then === \"function\") {\n      return (async () => {\n        const promiseValue = await value;\n        return safeJson(promiseValue);\n      })();\n    }\n    return safeJson(value);\n  }\n};\n";
exports.source = source;