import { PassportTypePipe } from './passport-type.pipe';

describe('PassportTypePipe', () => {
  it('create an instance', () => {
    const pipe = new PassportTypePipe();
    expect(pipe).toBeTruthy();
  });
});
