// Class, so that we initialize the values of it.

export class ShopParams {
  // filtering
  brandId = 0;
  typeId = 0;
  // Api doesn`t have pre-defined sortings like the brand & type
  sort = 'name';

  pageNumber = 1;
  pageSize = 6;

}