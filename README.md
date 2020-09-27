# Blom.LinqExtensions

Contains improvements for making your Linq great again.

### IQueryable projection with exclusion

Helps you out when making projections by enabling you to use exclusion instead of the regular inclusion pattern given by Select.
This is can be useful in conjunction with EntityframeworkCore as well when you want to your optimize sql queries.

#### Examples:

```C#
// Here the FileBytes are discarded from EF query translation and left as null
var doc = _db.Documents.Exclude(x => x.FileBytes).FirstOrDefault(x => x.Id == id);
```
