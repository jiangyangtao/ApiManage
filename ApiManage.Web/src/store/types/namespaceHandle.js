export default function removeNamespace(namespace, data) {
  const json = {};
  for (const i in data) {
    const item = data[i];
    const subJson = {};
    for (const key in item) {
      subJson[key] = item[key].replace(namespace, '');
    }
    json[i] = subJson;
  }
  return json;
}
