import { beforeEach, describe, expect, it, vi } from 'vitest';
import createFetchMock from 'vitest-fetch-mock';
import { homePageDataLoader } from './HomePageDataLoader';

const fetchMocker = createFetchMock(vi);
fetchMocker.enableMocks();

describe('HomePageDataLoader', () => {
  describe('when the api responds successfully', () => {
    const planets = [
      { id: 'mars', name: 'Mars' },
      { id: 'earth', name: 'Earth' },
    ];

    beforeEach(() => {
      fetchMocker.mockResponse(JSON.stringify(planets));
    });

    it('should return the deserialised planets', async () => {
      const result = await homePageDataLoader();

      expect(result.planets).toEqual(planets);
    })
  })

  describe('when the api responds unsuccessfully', () => {
    beforeEach(() => {
      fetchMocker.mockReject(new Error('Failed to fetch'));
    })

    it('should throw an error', async () => {
      await expect(async () => await homePageDataLoader()).rejects.toThrowError();
    })
  })
})