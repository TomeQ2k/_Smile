import { pageSize } from 'src/environments/environment';

export abstract class PaginationRequest {
  pageNumber = 1;
  pageSize: number = pageSize;
}
